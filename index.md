# Sagan Bot

Un chat bot sobre astronomía creado con [Microsoft Bot Framework](https://dev.botframework.com/) capaz de enviar información a través de tarjetas y calcular edad relativa en otros planetas utilizando date time recognizer

### Capturas del Bot

Algunas capturas del bot con algunos fragmentos de código dados para contextualizar el funcionamiento de este

![Inicio del bot](https://i.imgur.com/sydtUbp.png)


```C#

public static HeroCard GetWelcomeHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Sagan Bot",
                Subtitle = "El Bot de astronomia",
                Text = " Bienvenido(a). Estoy aqui para enseñarte un poco de astronomia",
                Images = new List<CardImage> { new CardImage("imagenInicio") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.MessageBack, "Enseñame", value: "Empezar") },
            };

            return heroCard;
        }
        
```

![Opciones](https://i.imgur.com/N8Hpgxe.png)

```C#

new PromptOptions
                  {
                      Prompt = MessageFactory.Text("¿Qué quieres aprender hoy?"), //Frase inicial de la pregunta
                      RetryPrompt = MessageFactory.Text("No entendi eso.  Por favor elija una opción de la lista."), //en caso de elegir una pcion inexistente se encia este mensaje
                      Choices = GetChoices(), //opciones
                  }, cancellationToken);

```            

```C#

 private IList<Choice> GetChoices()
        {
            var Options = new List<Choice>()
            {
                new Choice() { Value = "Estrellas", Synonyms = new List<string>() { "Quiero conocer las estrellas","Algo sobre las estrellas", "Cuentame sobre las estrellas" } },
                new Choice() { Value = "Planetas", Synonyms = new List<string>() { "Algo sobre los planetas","Cuentame sobre los planetas" } },
                new Choice() { Value = "Calculadora de edad", Synonyms = new List<string>() { "Quiero calcular mi edad", "mi edad en otro planeta","conocer mi edad en otro planeta", "Calcular mi edad" } },
                new Choice() { Value = "Universo", Synonyms = new List<string>() { "Quiero conocer el universo", "Sobre el universo","Cuentame sobre el universo" } },
                new Choice() { Value = "Cancelar", Synonyms = new List<string>() { "Atras","Adios","Volver" } },
            };

            return Options;
        }

```

For more details see [Basic writing and formatting syntax](https://docs.github.com/en/github/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/RaymoEf/saganBot/settings/pages). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://docs.github.com/categories/github-pages-basics/) or [contact support](https://support.github.com/contact) and we’ll help you sort it out.
