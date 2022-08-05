using System.Threading.Tasks.Dataflow;
using Derbyzone.Config;
using Derbyzone.Dto;
using Derbyzone.Generators;
using Derbyzone.Sender;
using Microsoft.Extensions.Options;

namespace Derbyzone;
public class OffersOrchestrator : IOffersOrchestrator
{
    private readonly IOfferGenerator _offerGenerator;
    private readonly IClientSender _clientSender;
    private readonly IStorageSender _storageSender;
    private readonly OrchestratorConfig _orchestratorConfig;

    public OffersOrchestrator(
        IOfferGenerator offerGenerator,
        IClientSender clientSender,
        IStorageSender storageSender,
        IOptions<OrchestratorConfig> orchestratorConfig)
    {
        _offerGenerator = offerGenerator;
        _clientSender = clientSender;
        _storageSender = storageSender;
        _orchestratorConfig = orchestratorConfig.Value;
    }

    public async Task OrchestrateAsync()
    {
        var broadcastBlock = new BroadcastBlock<Offer>(o => o);
        var actionBlockOptions = CreateActionBlockOptions();
        var sendToStorageActionBlock = new ActionBlock<Offer>(SendOfferToStorage, actionBlockOptions);
        var sendToClientActionBlock = new ActionBlock<Offer>(SendOfferToClient, actionBlockOptions);

        var linkOptions = CreateLinkOptions();
        broadcastBlock.LinkTo(sendToStorageActionBlock, linkOptions);
        broadcastBlock.LinkTo(sendToClientActionBlock, linkOptions);

        for (ulong i = 0; i < _orchestratorConfig.OffersToGenerate; i++)
        {
            var offer = _offerGenerator.GenerateOfffer();

            broadcastBlock.Post(offer);
        }

        broadcastBlock.Complete();

        var tasks = new[]
        {
            sendToStorageActionBlock.Completion,
            sendToClientActionBlock.Completion
        };

        await Task.WhenAll(tasks);
    }

    private Task SendOfferToStorage(Offer offer) =>
         _storageSender.SendAsync(offer);

    private Task SendOfferToClient(Offer offer) =>
        _clientSender.SendAsync(offer);

    private ExecutionDataflowBlockOptions CreateActionBlockOptions() =>
        new()
        {
            MaxDegreeOfParallelism = _orchestratorConfig.MaxDegreeOfParalelism,
        };
    private DataflowLinkOptions CreateLinkOptions() =>
        new()
        {
            PropagateCompletion = true
        };
}