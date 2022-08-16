
using Common.Dto.Derbyzone;

namespace SearchTests;

public class UnitTest1
{
    [Fact]
    public void Returns_equals_true_when_iterate_a_list_with_same_values()
    {
        var cxps1 = new[]
        {
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(3),
                PenaltyPercentage =  50
            },
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(4),
                PenaltyPercentage =  100
            }
        };
        var cxps2 = new[]
        {
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(3),
                PenaltyPercentage =  50
            },
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(4),
                PenaltyPercentage =  100
            }
        };

        for (var i = 0; i < cxps1.Count(); i++)
        {
            Assert.Equal(cxps1.ElementAt(i), cxps2.ElementAt(i));
        }
    }

    [Fact]
    public void Returns_not_equals_true_when_iterate_a_list_with_different_values()
    {
        var cxps1 = new[]
        {
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(3),
                PenaltyPercentage =  50
            },
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(4),
                PenaltyPercentage =  100
            }
        };
        var cxps2 = new[]
        {
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(3),
                PenaltyPercentage =  60
            },
            new CancellationPolicy()
            {
                DateFrom = DateTime.Today.AddDays(4),
                PenaltyPercentage =  100
            }
        };

        Assert.NotEqual(cxps1.ElementAt(0), cxps2.ElementAt(0));
        Assert.Equal(cxps1.ElementAt(1), cxps2.ElementAt(1));
    }
}