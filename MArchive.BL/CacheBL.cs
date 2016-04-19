using System.Threading;
namespace MArchive.BL {
	public class CacheBL {
		internal static void InitializeCache ( ) {
            //Thread initializingThread = new Thread ( InitializeThread );
            //initializingThread.Start ( );
		}

		private static void InitializeThread ( ) {
            int waitTime = 3;

            Thread.Sleep(500);
            MovieBL.GetAll();

            Thread.Sleep(waitTime);
            ActorBL.GetAll();

            Thread.Sleep(waitTime);
            DirectorBL.GetAll();

            Thread.Sleep(waitTime);
            WriterBL.GetAll();

            Thread.Sleep(waitTime);
            LanguageBL.GetAll();

            Thread.Sleep(waitTime);
            ArchiveBL.GetAll();

            Thread.Sleep(waitTime);
            TypeBL.GetAll();

            Thread.Sleep(waitTime);
			MovieActorBL.GetAll ( );
			MovieActorBL.GetAllDO ( );

            Thread.Sleep(waitTime);
            MovieUserArchiveBL.GetAll();
            MovieUserArchiveBL.GetAllDO();

            Thread.Sleep(waitTime);
			MovieDirectorBL.GetAll ( );
			MovieDirectorBL.GetAllDO ( );

            Thread.Sleep(waitTime);
            MovieLanguageBL.GetAll();
            MovieLanguageBL.GetAllDO();

            Thread.Sleep(waitTime);
            MovieNameBL.GetAll();
            MovieNameBL.GetAllAsDO();

            Thread.Sleep(waitTime);
            MovieTypeBL.GetAll();
            MovieTypeBL.GetAllDO();

            Thread.Sleep(waitTime);
            MovieWriterBL.GetAll();
            MovieWriterBL.GetAllDO();

            Thread.Sleep(waitTime);
            MovieUserRatingBL.GetAll();
		}
	}
}