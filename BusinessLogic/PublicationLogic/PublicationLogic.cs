namespace BusinessLogic.PublicationLogic
{
    public class PublicationLogic
    {
        public RecommandationSystem recommandationSystem;
        public PublicationLogic() 
        {
            recommandationSystem = new RecommandationSystem();
        }
        public async Task List()
        {
            await recommandationSystem.Recomandate();
        }

        public async Task Get()
        {

        }

        public async Task Create()
        {

        }

        public async Task Update()
        {

        }

        public async Task AddLike()
        {

        }

        public async Task AddComment()
        {

        }

        public async Task Delete()
        {

        }

        public async Task DeleteComment()
        {

        }

        public async Task DeleteLike()
        {

        }
    }
}
