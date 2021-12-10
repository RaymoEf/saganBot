// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.6.2

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace saganBot.Bots
{
    public class DialogAndWelcomeBot<T> : DialogBot<T>
        where T : Dialog
    {
        public DialogAndWelcomeBot(ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger)
            : base(conversationState, userState, dialog, logger)
        {
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                // Greet anyone that was not the target (recipient) of this message.
                // To learn more about Adaptive Cards, see https://aka.ms/msbot-adaptivecards for more details.
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var attachments = new List<Attachment>();
                    var welcomeCard = GetWelcomeHeroCard();
                    var response = MessageFactory.Attachment(attachments);
                    response.Attachments.Add(welcomeCard.ToAttachment());
                    await turnContext.SendActivityAsync(response, cancellationToken);
                    //await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>("DialogState"), cancellationToken);
                }
            }
        }

        // Load attachment from embedded resource.
        public static HeroCard GetWelcomeHeroCard()
        {
            //DateTime dateTime = DateTime.Now;
            var heroCard = new HeroCard
            {
                Title = "Sagan Bot",
                Subtitle = "El Bot de astronomia",
                Text = " Bienvenido(a). Estoy aqui para enseñarte un poco de astronomia",
                Images = new List<CardImage> { new CardImage("https://thumbs-prod.si-cdn.com/WYIt6L1TGX8nw55rANndeHBpkbc=/800x600/filters:no_upscale():focal(530x193:531x194)/https://public-media.smithsonianmag.com/filer/9a/c6/9ac678fc-5f74-4c6f-9e1f-1effe9b2e1b0/01-star-power-carl-sagan.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.MessageBack, "Enseñame", value: "Empezar") },
            };

            return heroCard;
        }
    }
}

