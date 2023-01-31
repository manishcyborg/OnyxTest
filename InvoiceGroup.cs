class InvoiceGroup
{
    public DateTime IssueDate { get; set; }
    public List<Invoice> Invoices { get; set; }
}

1 a. Create a LINQ expression for all guest names that occur on more than once (across all invoice groups and invoices, not per invoice group or invoice)
1 b .Create a LINQ expression for the total number of nights per travel agent for invoice groups issued in 2015

Solution for Question 1 a

Option1:

var guestNames = invoiceGroups
    .SelectMany(ig => ig.Invoices)
    .SelectMany(i => i.Observations)
    .GroupBy(o => o.GuestName)
    .Where(g => g.Count() > 1)
    .Select(g => g.Key)
    .ToList();

Option 2:

Dim guestNames = invoiceGroups _
    .SelectMany(Function(ig) ig.Invoices) _
    .SelectMany(Function(i) i.Observations) _
    .GroupBy(Function(o) o.GuestName) _
    .Where(Function(g) g.Count() > 1) _
    .Select(Function(g) g.Key) _
    .ToList()


Solution for Question 1 b

Option1:
var totalNightsPerAgent = invoiceGroups
    .Where(ig => ig.IssueDate.Year == 2015)
    .SelectMany(ig => ig.Invoices)
    .SelectMany(i => i.Observations)
    .GroupBy(o => o.TravelAgent)
    .Select(g => new TravelAgentInfo
    {
        TravelAgent = g.Key,
        TotalNumberOfNights = g.Sum(o => o.NumberOfNights)
    })
    .ToList();


Dim totalNightsPerAgent = invoiceGroups _
    .Where(Function(ig) ig.IssueDate.Year = 2015) _
    .SelectMany(Function(ig) ig.Invoices) _
    .SelectMany(Function(i) i.Observations) _
    .GroupBy(Function(o) o.TravelAgent) _
    .Select(Function(g) New TravelAgentInfo With {
        .TravelAgent = g.Key,
        .TotalNumberOfNights = g.Sum(Function(o) o.NumberOfNights)
    }) _
    .ToList()