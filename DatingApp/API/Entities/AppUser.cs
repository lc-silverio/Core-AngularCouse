namespace API.Entities
{
    public class AppUser
    {
        /// <summary>
        /// The user id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The password hash
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// The password salt
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}
