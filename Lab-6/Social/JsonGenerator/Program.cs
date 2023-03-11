// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Bogus;
using JsonGenerator;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

Console.WriteLine("Hello, World!");

var data = new DataGenerator();
data.UsersGenerator(100);
data.FriendsGenerator(1000);
data.MessageGenerator(200);
var usersData = data.users;
var friendsData = data.friends;
var messagesData = data.messages;
var options = new JsonSerializerOptions { WriteIndented = true };
var usersDataJson = JsonSerializer.Serialize(usersData, options);
var friendsDataJson = JsonSerializer.Serialize(friendsData, options);
var messagesDataJson = JsonSerializer.Serialize(messagesData, options);
File.WriteAllText(@"C:\DevCourse\Lab-6\Social\Data\users1.json", usersDataJson);
File.WriteAllText(@"C:\DevCourse\Lab-6\Social\Data\friends1.json", friendsDataJson);
File.WriteAllText(@"C:\DevCourse\Lab-6\Social\Data\messages1.json", messagesDataJson);
