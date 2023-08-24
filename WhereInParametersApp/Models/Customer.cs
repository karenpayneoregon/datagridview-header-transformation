using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WhereInParametersApp.Models;
internal class Customer : INotifyPropertyChanged
{
    private string _companyName;
    private string _street;
    private string _city;
    private int _countryIdentifier;
    private string _phone;
    private int _contactId;
    public int CustomerIdentifier { get; set; }

    public string CompanyName
    {
        get => _companyName;
        set
        {
            if (value == _companyName) return;
            _companyName = value;
            OnPropertyChanged();
        }
    }

    public string Street
    {
        get => _street;
        set
        {
            if (value == _street) return;
            _street = value;
            OnPropertyChanged();
        }
    }

    public string City
    {
        get => _city;
        set
        {
            if (value == _city) return;
            _city = value;
            OnPropertyChanged();
        }
    }

    public int CountryIdentifier
    {
        get => _countryIdentifier;
        set
        {
            if (value == _countryIdentifier) return;
            _countryIdentifier = value;
            OnPropertyChanged();
        }
    }

    public string CountryName { get; set; }

    public string Phone
    {
        get => _phone;
        set
        {
            if (value == _phone) return;
            _phone = value;
            OnPropertyChanged();
        }
    }

    public int ContactId
    {
        get => _contactId;
        set
        {
            if (value == _contactId) return;
            _contactId = value;
            OnPropertyChanged();
        }
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public override string ToString() => $"{CustomerIdentifier, - 5}{CompanyName}";


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
