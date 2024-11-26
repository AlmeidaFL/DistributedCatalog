namespace OrderService.Application.Exceptions;

public class LackOfProductOnCartException(string message) : Exception(message);