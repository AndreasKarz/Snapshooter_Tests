using Snapshooter.Xunit;
using System.Text.Json;

namespace Snapshooter_Tests
{
    public class SnapshooterUnitTest
    {

        [Fact]
        public void ObjectMatch()
        {
            var MyStudent = new Student(1, "abc"); 
            Snapshot.Match(MyStudent);
        }

        [Fact] 
        public void Todos()
        {
            var todoJson = GetUser();
            Snapshot.Match(todoJson);
        }

        private static string GetUser()
        {
            var url = "https://jsonplaceholder.typicode.com/users/2";

            using HttpClient client = new();
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            JsonDocument jsonDoc = JsonDocument.Parse(responseBody);
            return responseBody;
        }
    }

    internal record Student(int StudentId, string StudentName);
}