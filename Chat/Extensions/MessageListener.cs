using Chat.Consumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Chat.Hubs;

namespace Chat.Extensions
{
    public static class MessageListener
    {
        public static ChatConsumer ChatConsumer { get; set; }

        public static IApplicationBuilder UseMessageListener(this IApplicationBuilder app)
        {
            ChatConsumer = app.ApplicationServices.GetService<ChatConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            return app;
        }

        private static void OnStarted()
        {
            ChatConsumer.Consume();
        }
    }
}
