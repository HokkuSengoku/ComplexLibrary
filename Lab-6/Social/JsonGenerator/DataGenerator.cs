namespace JsonGenerator;

using Bogus;

public class DataGenerator
{
    public List<User> users = new List<User>();
    public List<Friend> friends = new List<Friend>();
    public List<Message> messages = new List<Message>();
    private Random _random = new Random();
    private DateTime _start;
    

    public void UsersGenerator(int count)
    {
        _start = new DateTime(1960, 1, 1);
        var range = (DateTime.Today - _start).Days;
        for (var i = 6; i < count + 6; i++)
        {
            users.Add(new Faker<User>()
                .RuleFor(user => user.DateOfBirth,
                    f => _start.AddDays(_random.Next(range)).AddHours(_random.Next(0, 24))
                        .AddMinutes(_random.Next(0, 60)).AddSeconds(_random.Next(0, 60)))
                .RuleFor(user => user.UserId, f => i)
                .RuleFor(user => user.Gender, f => f.Random.Int(0, 1))
                .RuleFor(user => user.Name, f => f.Name.FirstName())
                .RuleFor(user => user.Online, f => f.Random.Bool())
                .RuleFor(user => user.LastVisit,
                    f => _start.AddDays(_random.Next(range)).AddHours(_random.Next(0, 24))
                        .AddMinutes(_random.Next(0, 60)).AddSeconds(_random.Next(0, 60))));

        }
    }

    public void FriendsGenerator(int count)
    {
        _start = new DateTime(2000, 1, 1);
        var range = (DateTime.Today - _start).Days;

        for (var i = 0; i < count; i++)
        {
            friends.Add(new Faker<Friend>()
                .RuleFor(user => user.FromUserId, f => f.Random.Int(0, 100))
                .RuleFor(user => user.SendDate, f => _start.AddDays(_random.Next(range)).AddHours(_random.Next(0, 24))
                    .AddMinutes(_random.Next(0, 60)).AddSeconds(_random.Next(0, 60)))
                .RuleFor(user => user.Status, f => f.Random.Int(0, 3))
                .RuleFor(user => user.ToUserId, f => f.Random.Int(0, 100)));
        }
    }

    public void MessageGenerator(int count)
    {
        _start = new DateTime(2000, 1, 1);
        var range = (DateTime.Today - _start).Days;
        var listCount = _random.Next(0, 10);


        for (var i = 6; i < count + 6; i++)
        {
            messages.Add(new Faker<Message>()
                .RuleFor(message => message.AuthorId, f => f.Random.Int(0, 100))
                .RuleFor(message => message.Likes, f => LikesGenerator(listCount))
                .RuleFor(message => message.MessageId, f => i)
                .RuleFor(message => message.SendDate, f => _start.AddDays(_random.Next(range)).AddHours(_random.Next(0, 24))
                    .AddMinutes(_random.Next(0, 60)).AddSeconds(_random.Next(0, 60)))
                .RuleFor(message => message.Text, f => f.Lorem.Sentence()));
        }
    }

    public List<int> LikesGenerator(int count)
    {
        List<int> likes = new List<int>();
        
        for (var i = 0; i < count; i++)
        {
            likes.Add(_random.Next(0, 200));
        }

        return likes;
    }
}