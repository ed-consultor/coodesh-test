using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using System;
using System.Linq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class OrderValidatorTests
{
    [Fact(DisplayName = "Valid create command should pass validation")]
    public void Given_ValidCreateCommand_When_Validated_ShouldNotHaveErrors()
    {
        var order = OrderTestData.GenerateValidOrder(itemsCount: 2);

        var command = new CreateOrderCommand
        {
            SaleNumber = order.SaleNumber,
            Items = order.Items.Select(i => new CreateOrderItem
            {
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                TotalItemAmount = i.TotalItemAmount
            }),
            TotalAmount = order.TotalAmount,
            SaleDate = order.SaleDate,
            Branch = order.Branch,
            Customer = order.Customer
        };

        var validator = new CreateOrderCommandValidator();
        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Create command with empty sale number should fail")]
    public void Given_CreateCommandWithEmptySaleNumber_When_Validated_ShouldHaveError()
    {
        var command = new CreateOrderCommand();
        var validator = new CreateOrderCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
        result.ShouldHaveValidationErrorFor(x => x.Items);
        result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
    }

    [Fact(DisplayName = "Create command with future sale date should fail")]
    public void Given_CreateCommandWithFutureDate_When_Validated_ShouldHaveError()
    {
        var order = OrderTestData.GenerateValidOrder();
        var command = new CreateOrderCommand
        {
            SaleNumber = order.SaleNumber,
            Items = order.Items.Select(i => new CreateOrderItem
            {
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }),
            TotalAmount = order.TotalAmount,
            SaleDate = DateTime.UtcNow.AddDays(1), // future
            Branch = order.Branch,
            Customer = order.Customer
        };

        var validator = new CreateOrderCommandValidator();
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.SaleDate);
    }

    [Fact(DisplayName = "Create command with invalid item values should fail")]
    public void Given_CreateCommandWithInvalidItems_When_Validated_ShouldHaveError()
    {
        var command = new CreateOrderCommand
        {
            SaleNumber = "SN001",
            Items = new[] { new CreateOrderItem { Product = "", Quantity = 0, UnitPrice = 0 } },
            TotalAmount = 0,
            SaleDate = DateTime.UtcNow,
            Branch = "B",
            Customer = "C"
        };

        var validator = new CreateOrderCommandValidator();
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor("Items[0].Product");
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        result.ShouldHaveValidationErrorFor("Items[0].UnitPrice");
        result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
    }

    [Fact(DisplayName = "Valid update command should pass validation")]
    public void Given_ValidUpdateCommand_When_Validated_ShouldNotHaveErrors()
    {
        var order = OrderTestData.GenerateValidOrder(itemsCount: 2);

        var command = new UpdateOrderCommand
        {
            SaleNumber = order.SaleNumber,
            Items = order.Items.Select(i => new UpdateOrderItem
            {
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                TotalItemAmount = i.TotalItemAmount
            }),
            TotalAmount = order.TotalAmount,
            SaleDate = order.SaleDate,
            Branch = order.Branch,
            Customer = order.Customer
        };

        var validator = new UpdateOrderCommandValidator();
        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Update command missing sale number should fail")]
    public void Given_UpdateCommandMissingSaleNumber_When_Validated_ShouldHaveError()
    {
        var command = new UpdateOrderCommand();
        var validator = new UpdateOrderCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
    }
}
