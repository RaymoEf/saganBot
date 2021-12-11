# Sagan Bot

Un chat bot sobre astronomía creado con [Microsoft Bot Framework](https://dev.botframework.com/) capaz de enviar información a través de tarjetas y calcular edad relativa en otros planetas utilizando date time recognizer

### Markdown

Markdown is a lightweight and easy-to-use syntax for styling your writing. It includes conventions for

![Inicio del bot](https://imgur.com/sydtUbp)

```
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


```markdown
Syntax highlighted code block

# Header 1
## Header 2
### Header 3

- Bulleted
- List

1. Numbered
2. List

**Bold** and _Italic_ and `Code` text

[Link](url) and ![Image](src)
```

For more details see [Basic writing and formatting syntax](https://docs.github.com/en/github/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/RaymoEf/saganBot/settings/pages). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://docs.github.com/categories/github-pages-basics/) or [contact support](https://support.github.com/contact) and we’ll help you sort it out.
