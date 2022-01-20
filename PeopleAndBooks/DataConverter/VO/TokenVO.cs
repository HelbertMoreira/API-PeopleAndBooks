namespace PeopleAndBooks.DataConverter.VO
{
    public class TokenVO
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefrashToken { get; set; }

        public TokenVO(bool authenticated, string created, string expiration, string accessToken, string refrashToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefrashToken = refrashToken;
        }
    }
}
