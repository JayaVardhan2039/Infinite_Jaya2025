
CREATE PROCEDURE GetCustomersByCountry
    @Country VARCHAR(50)
AS
BEGIN
    SELECT CustomerID, CompanyName, ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax
    FROM Customers
    WHERE Country = @Country
END

select * from customers