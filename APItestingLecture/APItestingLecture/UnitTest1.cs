using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API_Tests_GitHub
{
    public class Tests
    {
        const string GitHubAPIUsername = "GeorgiIT";
        const string GitHubAPIPass = "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";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_GitHubAPI_GetIssuesByRepo()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");

            var request = new RestRequest("", Method.Get);
            request.Timeout = 3000;

            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Assert.IsTrue(response.ContentType.StartsWith("application/json"));

            var issues = JsonConvert.DeserializeObject<List<IssueResponse>>(response.Content);



            Assert.Pass();
        }

        [Test]

        
        public void Test_GitHubAPI_Creating_Issue()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            var request = new RestRequest("", Method.Post);
            request.Timeout = 3000;

            client.Authenticator = new HttpBasicAuthenticator(GitHubAPIUsername, GitHubAPIPass);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                title = "Trying my best here...",
                body = "6 hours debugging my code...",
                labels = new string[] { "bug", "importance:low", "type:UI" }
            });


            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));

            var issue = JsonConvert.DeserializeObject<List<IssueResponse>>(response.Content);

            Assert.IsTrue(issue.id > 0);
            Assert.IsTrue(issue.number > 0);
            Assert.IsTrue(!String.IsNullOrEmpty(issue.title));
            Assert.IsTrue(!String.IsNullOrEmpty(issue.body));

        }

        
    }
}

namespace API_Tests_GitHub
{
    public class IssueResponse
    {
        public IssueResponse() { }
        public long id { get; set; }
        public long number { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}

namespace API_Tests_GitHub
{
    internal class CommentResponse
    {
        public long id { get; set; }
        public string body { get; set; }
    }
}