using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Blog.Data;
using Blog.Data.Interface;
using Blog.Data.Model;
using Blog.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;

namespace LayForBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //ef框架连接字符串
            EFContext.ConString = Configuration.GetConnectionString("SqlServer");
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddMvc();
            //创建autofac管理注册类的容器实例
            var builder = new ContainerBuilder();
            builder.Populate(services);                     
            //将仓储注册到容器中
            builder.RegisterGeneric(typeof(EFRepositoryBase<,>)).As(typeof(IRepository<,>))
                .PropertiesAutowired().InstancePerLifetimeScope();
            Type baseType = typeof(IDependency);  
            Assembly[] assemblie = FindAll();
            //var assemblys = Assembly.Load("Blog.Data");
            builder.RegisterAssemblyTypes(assemblie)
             .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            //注册控制器         
            //var assemblie = Assembly.Load("LayForBlog");
            var IControllerType = typeof(Controller);
            builder.RegisterAssemblyTypes(assemblie).Where(t =>
                            IControllerType.IsAssignableFrom(t) && t != IControllerType).PropertiesAutowired().InstancePerLifetimeScope();
            //生成容器
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"Assets")),
                RequestPath = new PathString("/Assets")
            });
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static Assembly[] FindAll()
        {
            Assembly[] assemblyArray = Directory.GetFiles(GetBinPath(), "*.dll", SearchOption.TopDirectoryOnly).Concat<string>(Directory.GetFiles(GetBinPath(), "*.exe", SearchOption.TopDirectoryOnly)).ToArray<string>().Select<string, Assembly>(new Func<string, Assembly>(Assembly.LoadFrom)).Distinct<Assembly>().ToArray<Assembly>();
            return assemblyArray;
        }

        public static string GetBinPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //if (baseDirectory != (Environment.CurrentDirectory + @"\"))
            //{
            //    return Path.Combine(baseDirectory, "bin");
            //}
            return baseDirectory;
        }
    }
}
