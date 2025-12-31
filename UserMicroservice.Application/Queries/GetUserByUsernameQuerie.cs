namespace UserMicroservice.Application.Queries
{
    public class GetUserByUsernameQuery
    {
        public string Username { get; set; }
        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
