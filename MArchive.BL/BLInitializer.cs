using AutoMapper;
using MArchive.DataContext;
using MArchive.Domain.Lookup;
using MArchive.Domain.Movie;
using MArchive.Domain.User;

namespace MArchive.BL
{
    public class BLInitializer
    {
        public static void Initialize()
        {
            InitializeAutoMapper();
			CacheBL.InitializeCache ( );
        }

        private static void InitializeAutoMapper()
        {
            Mapper.CreateMap<MOV_M_Movie, MovieDO>();
			
            //Mapper.CreateMap<MOV_M_Actor, MovieActorDO>( );
			Mapper.CreateMap<vMovieActor, MovieActorDO>( );
            //Mapper.CreateMap<MOV_M_Archive, MovieArchiveDO>( );
            Mapper.CreateMap<vMovieArchive, MovieUserArchiveDO>();
            //Mapper.CreateMap<MOV_M_Director, MovieDirectorDO>();
            Mapper.CreateMap<vMovieDirector, MovieDirectorDO>();
            //Mapper.CreateMap<MOV_M_Language, MovieLanguageDO>();
            Mapper.CreateMap<vMovieLanguage, MovieLanguageDO>();

            //Mapper.CreateMap<MOV_M_Name, MovieNameDO>();
            Mapper.CreateMap<vMovieName, MovieNameDO>();

            //Mapper.CreateMap<MOV_M_Type, MovieTypeDO>();
            Mapper.CreateMap<vMovieType, MovieTypeDO>();

            //Mapper.CreateMap<MOV_M_UserRating, MovieUserRatingDO>();
            Mapper.CreateMap<vMovieUserRating, MovieUserRatingDO>();

            //Mapper.CreateMap<MOV_M_Writer, MovieWriterDO>();
            Mapper.CreateMap<vMovieWriter, MovieWriterDO>();

			Mapper.CreateMap<INF_Actor, ActorDO> ( );
			Mapper.CreateMap<USR_Archive, ArchiveDO> ( );
			Mapper.CreateMap<INF_Director, DirectorDO> ( );
			Mapper.CreateMap<INF_Language, LanguageDO> ( );
			Mapper.CreateMap<INF_Type, TypeDO> ( );
			Mapper.CreateMap<INF_Writer, WriterDO> ( );

            Mapper.CreateMap<USR_List, UserListDO>();
            Mapper.CreateMap<USR_ListMovie, UserListMovieDO>();
            Mapper.CreateMap<vUserListMoviesDetail, UserListDetailDO>();

            Mapper.CreateMap<User, UserDO>();
        }
    }
}