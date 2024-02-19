namespace BankConsole;

public class Client : User, IPerson
{
    public char TaxRegime { get; set; }

    public Client() { }

    public Client(int ID, string name, string email, decimal Balance, char TaxRegime) : base(ID, name, email, Balance)
    {
        this.TaxRegime = TaxRegime;
        SetBalance(Balance);
    }

    public override void SetBalance(decimal amount)
    {
        base.SetBalance(amount);

        if (TaxRegime.Equals('M'))
            Balance += (amount * 0.02m);
    }
    public override string ShowData()
    {
        return base.ShowData() + $", Régimen fiscal: {TaxRegime}";
    }

    public string GetName()
    {
        return Name;
    }

    public string GetCountry()
    {
        throw new NotImplementedException();
    }
}