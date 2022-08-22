using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Interfaces
{
	public interface ICustomer
	{
		IEnumerable<Customer> getAllCustomers();
		Customer GetCustomerById(int id);

		void SaveCustomer(Customer customer);

		void DeleteCustomer(int id);
	}
}