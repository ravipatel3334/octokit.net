﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class FollowersClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new FollowersClient(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "user/followers"));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetAll("alfhenrik");

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/followers"));
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null));
                AssertEx.Throws<ArgumentException>(async () => await client.GetAll(""));
            }
        }

        public class TheGetFollowingForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetFollowingForCurrent();

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "user/following"));
            }
        }

        public class TheGetFollowingMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.GetFollowing("alfhenrik");

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                AssertEx.Throws<ArgumentNullException>(async () => await client.GetFollowing(null));
                AssertEx.Throws<ArgumentException>(async () => await client.GetFollowing(""));
            }
        }

        public class TheIsFollowingForCurrentMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.IsFollowingForCurrent("alfhenrik");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                AssertEx.Throws<ApiException>(async () => await client.IsFollowingForCurrent("alfhenrik"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                AssertEx.Throws<ArgumentNullException>(async () => await client.IsFollowingForCurrent(null));
                AssertEx.Throws<ArgumentException>(async () => await client.IsFollowingForCurrent(""));
            }
        }

        public class TheIsFollowingMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following/alfhenrik-test"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.IsFollowing("alfhenrik", "alfhenrik-test");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Arg.Is<Uri>(u => u.ToString() == "users/alfhenrik/following/alfhenrik-test"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                AssertEx.Throws<ApiException>(async () => await client.IsFollowing("alfhenrik", "alfhenrik-test"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                AssertEx.Throws<ArgumentNullException>(async () => await client.IsFollowing(null,  "alfhenrik-test"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.IsFollowing("alfhenrik", null));
                AssertEx.Throws<ArgumentException>(async () => await client.IsFollowing("", "alfhenrik-text"));
                AssertEx.Throws<ArgumentException>(async () => await client.IsFollowing("alfhenrik", ""));
            }

        }

        public class TheFollowMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
                var connection = Substitute.For<IConnection>();
                connection.PutAsync<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    Args.Object).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                var result = await client.Follow("alfhenrik");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.PutAsync<object>(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"),
                    new { }).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new FollowersClient(apiConnection);

                AssertEx.Throws<ApiException>(async () => await client.Follow("alfhenrik"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Follow(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Follow(""));
            }
        }

        public class TheUnfollowMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                client.Unfollow("alfhenrik");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "user/following/alfhenrik"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new FollowersClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Unfollow(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Unfollow(""));
            }
        }
    }
}
