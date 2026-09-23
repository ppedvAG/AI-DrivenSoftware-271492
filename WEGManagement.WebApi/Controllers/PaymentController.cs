using Microsoft.AspNetCore.Mvc;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Models;
using WEGManagement.Domain.Repositories;

namespace WEGManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ICondoFeeRepository _condoFeeRepository;

    public PaymentController(
        IPaymentRepository paymentRepository,
        ICondoFeeRepository condoFeeRepository)
    {
        _paymentRepository = paymentRepository;
        _condoFeeRepository = condoFeeRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Payment>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var payment =
            await _paymentRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (payment is null)
            return NotFound();

        return Ok(payment);
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> Create(
        CreatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var condoFee =
            await _condoFeeRepository.GetByIdAsync(
                request.CondoFeeId,
                cancellationToken);

        if (condoFee is null)
            return BadRequest("Condo fee not found.");

        var payment = new Payment(
            request.Amount,
            request.Date,
            request.PaymentType,
            request.Reference,
            request.CondoFeeId);

        condoFee.MarkAsPaid(payment);

        await _paymentRepository.AddAsync(
            payment,
            cancellationToken);

        await _condoFeeRepository.UpdateAsync(
            condoFee,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = payment.Id },
            payment);
    }
}

public record CreatePaymentRequest(
    decimal Amount,
    DateTime Date,
    PaymentType PaymentType,
    string Reference,
    Guid CondoFeeId);
