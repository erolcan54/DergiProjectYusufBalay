using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.MemoryCaching;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.Extensions.Caching.Memory;

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

            builder.RegisterType<BasariManager>().As<IBasariService>().SingleInstance();
            builder.RegisterType<EfBasariDal>().As<IBasariDal>().SingleInstance();

            builder.RegisterType<KatalogManager>().As<IKatalogService>().SingleInstance();
            builder.RegisterType<EfKatalogDal>().As<IKatalogDal>().SingleInstance();

            builder.RegisterType<IcGorselManager>().As<IIcGorselService>().SingleInstance();
            builder.RegisterType<EfIcGorselDal>().As<IIcGorselDal>().SingleInstance();

            builder.RegisterType<DisGorselManager>().As<IDisGorselService>().SingleInstance();
            builder.RegisterType<EfDisGorselDal>().As<IDisGorselDal>().SingleInstance();

            builder.RegisterType<EtkinlikManager>().As<IEtkinlikService>().SingleInstance();
            builder.RegisterType<EfEtkinlikDal>().As <IEtkinlikDal>().SingleInstance();

            builder.RegisterType<EtkinlikResimManager>().As<IEtkinlikResimService>().SingleInstance();
            builder.RegisterType<EfEtkinlikResimDal>().As<IEtkinlikResimDal>().SingleInstance();

            builder.RegisterType<KulupManager>().As<IKulupService>().SingleInstance();
            builder.RegisterType<EfKulupDal>().As<IKulupDal>().SingleInstance();

            builder.RegisterType<KurumYorumManager>().As<IKurumYorumService>().SingleInstance();
            builder.RegisterType<EfKurumYorumDal>().As<IKurumYorumDal>().SingleInstance();

            builder.RegisterType<KurumYorumBegeniManager>().As<IKurumYorumBegeniService>().SingleInstance();
            builder.RegisterType<EfKurumYorumBegeniDal>().As<IKurumYorumBegeniDal>().SingleInstance();

            builder.RegisterType<KurumBeniAraManager>().As<IKurumBeniAraService>().SingleInstance();
            builder.RegisterType<EfKurumBeniAraDal>().As<IKurumBeniAraDal>().SingleInstance();

            builder.RegisterType<BurslulukSinavBasvuruManager>().As<IBurslulukSinavBasvuruService>().SingleInstance();
            builder.RegisterType<EfBurslulukSinavBasvuruDal>().As<IBurslulukSinavBasvuruDal>().SingleInstance();

            builder.RegisterType<isBasvuruManager>().As<IisBasvuruService>().SingleInstance();
            builder.RegisterType<EfisBasvuruDal>().As<IisBasvuruDal>().SingleInstance();

            builder.RegisterType<SliderManager>().As<ISliderService>().SingleInstance();
            builder.RegisterType<EfSliderDal>().As<ISliderDal>().SingleInstance();

            builder.RegisterType<KurumsalManager>().As<IKurumsalService>().SingleInstance();
            builder.RegisterType<EfKurumsalDal>().As<IKurumsalDal>().SingleInstance();

            builder.RegisterType<PopupModalManager>().As<IPopupModalService>().SingleInstance();
            builder.RegisterType<EfPopopModalDal>().As<IPopupModalDal>().SingleInstance();

            builder.RegisterType<UcretsizDanismanManager>().As<IUcretsizDanismanService>().SingleInstance();
            builder.RegisterType<EfUcretsizDanismanDal>().As<IUcretsizDanismanDal>().SingleInstance();

            var MemoryCacheInstance = new MemoryCache(new MemoryCacheOptions());
            builder.RegisterInstance<IMemoryCache>(MemoryCacheInstance);

            builder.RegisterType<CacheManager>().As<ICacheManager>().SingleInstance();

            //var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            //    .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            //    {
            //        Selector = new AspectInterceptorSelector()
            //    }).SingleInstance();


        }
    }
}
