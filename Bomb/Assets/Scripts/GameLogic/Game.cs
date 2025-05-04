using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Explosion
{
    private Game game;
    private float duration = 0;
    private bool isCountdown = false;

    public Explosion(Game game)
    {
       this.game = game;
    }

    public void init()
    {
        duration = 0;
        isCountdown = false;
    }

    public void Update(float dt)
    {
        if (!isCountdown)
        {
            if (duration > Constants.EXPLOSION_COUNTDOWN_TIME)
            {
                isCountdown = true;
                game.OnReadyToStart();
            }

            duration += dt;
        }
    }
}


public class Bomb
{
    private Game game;
    public Bomb(Game game)
    {
       this.game = game;
    }

    private float duration = 0;
    private float aliveTime = 0;
    private bool isAlerted = false;
    private bool isExploded = false;

    public void init()
    {
        var rand = new Random();
        aliveTime = Constants.MIN_BOMB_ALIVE_TIME + (float)(rand.NextDouble() * (Constants.MAX_BOMB_ALIVE_TIME - Constants.MIN_BOMB_ALIVE_TIME));
        Debug.Log("<><><> aliveTime aliveTime " + aliveTime.ToString() );
        duration = 0;
        isAlerted = false;
        isExploded = false;
    }

    public void tryAddBonusTime()
    {
        if (isAlerted)
        {
            aliveTime = duration + Constants.BONUS_BOMB_ALIVE_TIME;
        }
    }

    public void Update(float dt)
    {
        if (isExploded)
        {
            Debug.Log("ERROR isExploded!");
            return;
        }

        if (!isAlerted && (aliveTime - duration) < Constants.ALERT_BOMB_TIME)
        {
            game.onAlert();
            isAlerted = true;
        }

        if ((aliveTime - duration) <= 0)
        {
            game.onExplosion();
            isExploded = true;
            return;
        }

        duration += dt;
    }
}


public class Player
{
    public String Name { get; private set; }
    public int Score { get; private set; }
    
    public Player(String name)
    {
        Name = name;
        Score = 0;
    }
    public void explosion()
    {
        Score += 1;
    }

}


public struct Card
{
    public string Word { get; private set; }
    public WordCondition Condition { get; private set; }

    public Card(string word, WordCondition condition)
    {
        Word = word;
        Condition = condition;
    }
}


public class Game : MonoBehaviour
{
    public List<Player> players_ = new List<Player>();
    public int currentPlayerIndex = 0;
    public GameState State { get; private set; } = GameState.INACTIVE;
    public float gameTime_ = 0;
    public Queue<Card> cards = new Queue<Card>();
    public Card CurrentCard { get; private set; }
    Bomb bomb = null;
    Explosion explosion = null;

    public delegate void StateChanged(GameState state);
    public event StateChanged evStateChanged;

    public delegate void PlayerChanged();
    public event PlayerChanged evCurrentPlayerChanged;

    public delegate void Alert();
    public event Alert evAlert;
    
    private bool isBlockedPrevPlayer = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("<><><> Game.Start");
        
        bomb = new Bomb(this);
        explosion = new Explosion(this);

        UserPreferenceData userPreferenceData = UserPreference.Load();
        foreach (string playerName in userPreferenceData.players)
        {
            players_.Add(new Player(playerName));
        }

        List<string> cardsStrings = new List<string>(Constants.CARDS);
        cardsStrings.Shuffle();

        Random rand = new Random();
        var length = rand.Next(cardsStrings.Count);
        if (length == 0)
        {
            length += 1;
        }

        for (int i = 0; i < length; ++i)
        {
            var word = cardsStrings[i];
            cards.Enqueue(new Card(word, Utils.GetWordConditionRandom()));
        }

        currentPlayerIndex = rand.Next(players_.Count);

        nextCard();
        startRound();
        Debug.Log("<><><> currentPlayerIndex " + currentPlayerIndex);
    }

    public Player getCurrentPlayer()
    {
        return players_[currentPlayerIndex];
    }

    void setCurrentPlayerIndex(int index)
    {
        currentPlayerIndex = index;
        evCurrentPlayerChanged?.Invoke();
    }

    bool nextCard()
    {
        if (cards.Count == 0)
        {
            var result = GetResult();
            if (result.Count > 1 && result[0].Score == result[1].Score)
            {
                var rand = new Random();
                var index = rand.Next(Constants.CARDS.Length);
                cards.Enqueue(new Card(Constants.CARDS[index], Utils.GetWordConditionRandom()));
            }
            else
            {
                setState(GameState.RESULT);
                return false;
            }
        }
        CurrentCard = cards.Dequeue();
        return true;
    }

    public void nextPlayer()
    {
        isBlockedPrevPlayer = false;

        if (currentPlayerIndex == (players_.Count - 1))
        {
            setCurrentPlayerIndex(0);
        }
        else
        {
            setCurrentPlayerIndex(++currentPlayerIndex);
        }

        bomb.tryAddBonusTime();
    }

    public bool prevPlayer()
    {
        if (isBlockedPrevPlayer)
            return false;

        if (currentPlayerIndex == 0)
        {
            setCurrentPlayerIndex(players_.Count - 1);
        }
        else
        {
            setCurrentPlayerIndex(--currentPlayerIndex);
        }

        isBlockedPrevPlayer = true;
        return true;
    }

    public List<Player> GetResult()
    {
        List<Player> result = new List<Player>(players_);
        result.Sort(delegate (Player p1, Player p2)
        {
            return p1.Score.CompareTo(p2.Score);
        });
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameState.INACTIVE:
                break;
            case GameState.COUNTDOWN:
                if (gameTime_ >= Constants.COUNTDOWN_TIME)
                {
                    bomb.init();
                    explosion.init();
                    setState(GameState.PLAY);
                }
                break;
            case GameState.PLAY:
                bomb.Update(Time.deltaTime);
                break;
            case GameState.EXPLOSION:
                explosion.Update(Time.deltaTime);
                break;
            case GameState.RESULT:
                break;
        }
        
        gameTime_ += Time.deltaTime;
    }

    private void setState(GameState state)
    {
        State = state;
        evStateChanged?.Invoke(State);
    }

    public void startRound()
    {
        gameTime_ = 0;
        isBlockedPrevPlayer = true; ;
        setState(GameState.COUNTDOWN);
    } 
    
    public void onAlert()
    {
        evAlert?.Invoke();
    }    

    public void OnReadyToStart()
    {
        if (nextCard())
        {
            nextPlayer();
            setState(GameState.READY_TO_START);
        }
    }

    public void onExplosion()
    {
        var player = getCurrentPlayer();
        player.explosion();
        setState(GameState.EXPLOSION);
    }
}
