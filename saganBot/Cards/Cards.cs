using System.IO;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;


namespace saganBot.Cards
{
    public class Cards
    {
        public static Attachment mercurioCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "mercurio.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment venusCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "venus.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment marteCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "marte.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment jupiterCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "jupiter.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment saturnoCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "saturno.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment uranoCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "urano.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment neptunoCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "neptuno.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment solCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "sol.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment centauriACard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "centauriA.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment centauriBCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "CentauriB.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

        public static Attachment proximaCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "proxima.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }
        public static Attachment betelgeuseCard()
        {
            // combine path for cross platform support
            string[] paths = { ".", "Cards", "betelgeuse.json" }; //la ruta del archivo, solo cambiar el ultimo parametro por el nombre de tu archivo
            var adaptiveCardJson = File.ReadAllText(Path.Combine(paths));

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };
            return adaptiveCardAttachment;
        }

    }
}
