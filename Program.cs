BlackJack(new string[] { "ten", "king", "ace" });
Console.ReadLine();

static void BlackJack(string[] availableCards)
{
    var cardMappings = _initializeCardMappings();
    var (total, aceMinified) = _getTotal(cardMappings, availableCards);
    var outcome = _getOutcome(total);
    var highestCard = _getHighestCard(cardMappings, availableCards, aceMinified);

    Console.WriteLine($"{outcome} {highestCard}");
}

static string _getHighestCard(Dictionary<string, ValueObject> cardMappings, string[] availableCards, bool aceMinified)
{
    var currentHighestCard = "";
    var currentHighestCardValueObject = new ValueObject() { Rank = 0, Value = 0 };

    if (aceMinified)
    {
        cardMappings["ace"].Value = 1;
        cardMappings["ace"].Rank = 1;
    }

    for (int i = 0; i < availableCards.Length; i++)
    {
        var card = availableCards[i];
        var cardValueObject = cardMappings[card];

        if (cardValueObject.Value >= currentHighestCardValueObject.Value && cardValueObject.Rank > currentHighestCardValueObject.Rank)
        {
            currentHighestCard = card;
            currentHighestCardValueObject = cardValueObject;
        }
    }

    return currentHighestCard;
}

static Dictionary<string, ValueObject> _initializeCardMappings()
{
    var cardMappings = new Dictionary<string, ValueObject>()
    {
        {"two", new ValueObject(){Rank = 2, Value = 2}},
        {"three", new ValueObject(){Rank = 3, Value = 3}},
        {"four", new ValueObject(){Rank = 4, Value = 4}},
        {"five", new ValueObject(){Rank = 5, Value = 5}},
        {"six", new ValueObject(){Rank = 6, Value = 6}},
        {"seven", new ValueObject(){Rank = 7, Value = 7}},
        {"eight", new ValueObject(){Rank = 8, Value = 8}},
        {"nine", new ValueObject(){Rank = 9, Value = 9}},
        {"ten", new ValueObject(){Rank = 10, Value = 10}},
        {"jack", new ValueObject(){Rank = 11, Value = 10}},
        {"queen", new ValueObject(){Rank = 12, Value = 10}},
        {"king", new ValueObject(){Rank = 13, Value = 10}},
        {"ace", new ValueObject(){Rank = 14, Value = 11}},
    };

    return cardMappings;
}

static string _getOutcome(int total)
{
    return total switch
    {
        > 21 => "above",
        21 => "blackjack",
        < 21 => "below"
    };
}

static (int, bool) _getTotal(Dictionary<string, ValueObject> cardMappings, string[] availableCards)
{
    var threshold = 21;
    var containsAceInner = false;
    var aceMinified = false;
    var total = 0;

    for (int i = 0; i < availableCards.Length; i++)
    {
        var card = availableCards[i];

        if (card == "ace") containsAceInner = true;

        var cardValue = cardMappings[card].Value;
        total += cardValue;
    }

    if ((total > threshold) && containsAceInner)
    {
        total -= cardMappings["ace"].Value;
        total += 1;
        aceMinified = true;
    }

    return (total, aceMinified);
}

public class ValueObject
{
    public int Value { get; set; }
    public int Rank { get; set; }
}