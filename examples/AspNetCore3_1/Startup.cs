//===================================================
//  License: Apache-2.0
//  Contributors: yiyungent@gmail.com
//  Project: https://moeci.com/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PluginCore.AspNetCore.Extensions;

namespace AspNetCore3_1
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddPluginCore();

            //services.AddHttpContextAccessor();

            //services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".AdventureWorks.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(60*60*24);
            //    options.Cookie.IsEssential = true;
            //});

            // 允许 跨域请求
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                    builder =>
                    {
                        // 配置的白名单都允许
                        // https://www.cnblogs.com/jidanfan/p/11177509.html
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UsePluginCore();

            // UsePluginCore() 内部已经 Use 下面两个, 无需重复 Use
            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseSession();

            // 跨域: 启用 CORS 中间件 (若业务后台与服务端同源, 则不需要配置)
            // 注意: 这样启用, 会使所有路由 都使用此跨域策略
            app.UseCors(AllowSpecificOrigins);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
