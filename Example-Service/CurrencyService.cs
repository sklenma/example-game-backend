using System;
using Example_Persistance.Model;
using Example_Service.Enums;
using Example_Service.ValueObjects;

namespace Example_Service
{
    public interface ICurrencyService
    {
        ArticleVO AdjustCurrency(int articleId, int amount, Player player, bool remove = false);
    }

    public class CurrencyService : ICurrencyService
    {
        public bool IsCurrency(int articleId)
        {
            return (articleId >= 1001 && articleId < 2000);
        }

        public ArticleVO AdjustCurrency(int articleId, int amount, Player player, bool remove = false)
        {

            if (!IsCurrency(articleId))
            {
                return null;
            }

            switch (articleId)
            {
                case (int)CurrencyType.Gems:
                    if (remove && player.Gems < amount)
                        return null;

                    amount = remove ? (-1) * amount : amount;
                    player.Gems += amount;
                    return new ArticleVO((int) CurrencyType.Gems, player.Gems);
                case (int)CurrencyType.Gold:
                    if (remove && player.Gold < amount)
                        return null;

                    amount = remove ? (-1) * amount : amount;
                    player.Gold += amount;
                    return new ArticleVO((int) CurrencyType.Gold, player.Gold);
                case (int)CurrencyType.XP:

                    throw new Exception("Experience could not be added");
                case (int)CurrencyType.Level:
                    throw new Exception("Level could not be added or removed");
                default:
                    throw new Exception("Currency not supported");
            }
        }

        public CurrencyType GetCurrencyFromString(string currency)
        {
            if (currency.Equals("Gold"))
            {
                return CurrencyType.Gold;
            }
            else if (currency.Equals("Gems"))
            {
                return CurrencyType.Gems;
            }
            else
            {
                return CurrencyType.NONE;
            }
        }
    }
}