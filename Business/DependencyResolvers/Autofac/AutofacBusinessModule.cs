using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ilManager>().As<IilService>().SingleInstance();
            builder.RegisterType<EfilDal>().As<IilDal>().SingleInstance();

            builder.RegisterType<ilceManager>().As<IilceService>().SingleInstance();
            builder.RegisterType<EfilceDal>().As<IilceDal>().SingleInstance();

            builder.RegisterType<OkulTurManager>().As<IOkulTurService>().SingleInstance();
            builder.RegisterType<EfOkulTurDal>().As<IOkulTurDal>().SingleInstance();

            builder.RegisterType<OkulManager>().As<IOkulService>().SingleInstance();
            builder.RegisterType<EfOkulDal>().As<IOkulDal>().SingleInstance();

            builder.RegisterType<EgitimTurManager>().As<IEgitimTurService>().SingleInstance();
            builder.RegisterType<EfEgitimTurDal>().As<IEgitimTurDal>().SingleInstance();

            builder.RegisterType<BlogManager>().As<IBlogService>().SingleInstance();
            builder.RegisterType<EfBlogDal>().As<IBlogDal>().SingleInstance();

            builder.RegisterType<BransManager>().As<IBransService>().SingleInstance();
            builder.RegisterType<EfBransDal>().As<IBransDal>().SingleInstance();

            builder.RegisterType<BurslulukSinavManager>().As<IBurslulukSinavService>().SingleInstance();
            builder.RegisterType<EfBurslulukSinav>().As<IBurslulukSinavDal>().SingleInstance();

            builder.RegisterType<indirimManager>().As<IindirimService>().SingleInstance();
            builder.RegisterType<EfindirimDal>().As<IindirimDal>().SingleInstance();

            builder.RegisterType<OgretmenManager>().As<IOgretmenService>().SingleInstance();
            builder.RegisterType<EfOgretmenDal>().As<IOgretmenDal>().SingleInstance();

            builder.RegisterType<YoneticiManager>().As<IYoneticiService>().SingleInstance();
            builder.RegisterType<EfYoneticiDal>().As <IYoneticiDal>().SingleInstance();

            builder.RegisterType<KullaniciManager>().As<IKullaniciService>().SingleInstance();
            builder.RegisterType<EfKullaniciDal>().As<IKullaniciDal>().SingleInstance();

            builder.RegisterType<OzelDersOgretmenManager>().As<IOzelDersOgretmenService>().SingleInstance();
            builder.RegisterType<EfOzelDersOgretmenDal>().As<IOzelDersOretmenDal>().SingleInstance();

            builder.RegisterType<OzelDersVeliBasvuruManager>().As<IOzelDersVeliBasvuruService>().SingleInstance();
            builder.RegisterType<EfOzelDersVeliBasvuruDal>().As<IOzelDersVeliBasvuruDal>().SingleInstance();

            builder.RegisterType<OzelOgretmenYorumManager>().As<IOzelOgretmenYorumService>().SingleInstance();
            builder.RegisterType<EfOzelOgretmenYorumDal>().As<IOzelOgretmenYorumDal>().SingleInstance();

            builder.RegisterType<OzelOgretmenYorumBegeniManager>().As<IOzelOgretmenYorumBegeniService>().SingleInstance();
            builder.RegisterType<EfOzelOgretmenYorumBegeniDal>().As<IOzelOgretmenYorumBegeniDal>().SingleInstance();

            builder.RegisterType<EgitimModeliManager>().As<IEgitimModeliService>().SingleInstance();
            builder.RegisterType<EfEgitimModeliDal>().As<IEgitimModeliDal>().SingleInstance();

            builder.RegisterType<EgitimModeliResimManager>().As<IEgitimModeliResimService>().SingleInstance();
            builder.RegisterType<EfEgitimModeliResimDal>().As<IEgitimModeliResimDal>().SingleInstance();

            //var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            //    .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            //    {
            //        Selector = new AspectInterceptorSelector()
            //    }).SingleInstance();


        }
    }
}
