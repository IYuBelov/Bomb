using System.Collections.Generic;
using Random = System.Random;


public static class Events
{
    public const string
        evGameStateChanged = "evGameStateChanged";
}



public enum GameState
{
    INACTIVE,
    COUNTDOWN,
    PLAY,
    EXPLOSION,
    READY_TO_START,
    RESULT
}

public enum WordCondition
{
    BEGINING,
    ANYWHERE,
    END,
}


public static class Constants
{
    public const float COUNTDOWN_TIME = 5.0f;
    public const float MIN_BOMB_ALIVE_TIME = 10.0f;
    public const float MAX_BOMB_ALIVE_TIME = 60.0f;
    public const float BONUS_BOMB_ALIVE_TIME = 3;
    public const float ALERT_BOMB_TIME = 5;
    public const float EXPLOSION_COUNTDOWN_TIME = 5;

    public static readonly string[] CARDS =
    {
        "кар",
        "co",
        "метр",
        "инк",
        "ем",
        "ан",
        "бри",
        "бы",
        "вни",
        "гон",
        "до",
        "доз",
        "жи",
        "зик",
        "кас",
        "кос",
        "кра",
        "мер",
        "мол",
        "мор",
        "пра",
        "пре",
        "при",
        "рен",
        "ска",
        "сли",
        "соль",
        "тел",
        "фи",
        "ча",
        "чик",
        "чу",
        "ши",
        "ща",
        "щик",
        "щу",
        "чер",
        "шаб",
        "кон",
        "ноп",
        "нос",
        "май",
        "сонь",
        "лом",
        "пка",
        "тер",
        "ран",
        "от",
        "обо",
        "синь",
        "мой",
        "чок",
        "кет",
        "доч",
        "кет",
        "рад",
        "лон",
        "кат",
        "лец",
        "лат",
        "ник",
        "бен",
        "вец",
        "вод",
        "нал",
        "ряд",
        "кет",
        "ряд",
        "кет",
        "рат",
        "рог",
        "пог",
        "пок",
        "мя",
        "бан",
        "узд",
        "пах",
        "зем",
        "пле",
        "бра",
        "зна",
        "корь",
        "жар",
        "нец",
        "нок",
        "кав",
        "нок",
        "чаг",
        "рик",
        "хар",
        "сан",
        "виш",
        "сли",
        "сед",
        "гор",
        "шко",
        "рох",
        "мен",
        "сце",
        "рок",
        "чаш",
        "кус",
        "лин",
        "лод",
        "фра",
        "што",
        "сня",
        "лик",
        "нец"
    };
}
