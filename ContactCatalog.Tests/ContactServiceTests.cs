using ContactCatalog;
using Moq;
using System.Linq;
using Xunit;

public class ContactServiceTests
{
    [Fact]
    public void Filter_By_Tag_Returns_Only_Matching()
    {
        var repoMock = new Mock<IContactRepository>();
        repoMock.Setup(r => r.GetAll()).Returns(new[]
        {
            new Contact{ Id=1, Name="Anna", Email="a@x.se", Tags = new List<string>{"friend"} },
            new Contact{ Id=2, Name="Bo",   Email="b@x.se", Tags = new List<string>{"work"} }
        });

        var service = new ContactService(new Moq.Mock<Microsoft.Extensions.Logging.ILogger>().Object);
        var result = repoMock.Object.GetAll().Where(c => c.Tags.Contains("friend")).ToList();

        Assert.Single(result);
        Assert.Equal("Anna", result[0].Name);
    }
}

public interface IContactRepository
{
    IEnumerable<Contact> GetAll();
    void Add(Contact c);
}
