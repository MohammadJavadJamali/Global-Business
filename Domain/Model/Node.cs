

namespace Domain.Model
{
    public class Node
    {

        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser AppUser { get; set; }

        public string ParentId { get; set; }

        public string LeftUserId { get; set; }
        
        public string RightUserId { get; set; }

    }
}
