﻿namespace dotnet_API.Entities
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public bool FirstTimeLogin { get; set; }
        public bool IsSkipedFirstPage { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
