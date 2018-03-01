using MonitorNetwork.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace MonitorNetwork.BLL
{
    public class GenerateCreditCard
    {
        public const string VALID_CREDIT_CARDS_PATH = @"~/BLL/ValidCreditCards.txt";

        private readonly MNDatabase _dbContext;

        public GenerateCreditCard(MNDatabase dbContext)
        {
            _dbContext = dbContext;
        }


        public string GetValidUnusedCreditCard()
        {
            var validCreditCards = File.ReadLines(HostingEnvironment.MapPath(VALID_CREDIT_CARDS_PATH));
            string creditCardNumber = "";
            foreach(var validCreditCard in validCreditCards)
            {
                if (String.IsNullOrEmpty(_dbContext.creditcard.Select(x => x.cardNumber).FirstOrDefault(x => x == validCreditCard)))
                {
                    creditCardNumber = validCreditCard;
                    break;
                }
            }

            return creditCardNumber;
        }
    }
}