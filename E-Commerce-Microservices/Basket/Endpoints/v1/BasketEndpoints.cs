using Basket.Models;
using Basket.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Basket.Extensions;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Basket.Endpoints.v1
{
    public static class BasketEndpoints
    {
        public static void MapV1BasketEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/basket/v1")
                .WithTags("Basket");

            group.MapPost("/get", GetBasket)
                .WithName("Get Basket")
                .WithSummary("Get user's basket");

            group.MapPost("/add", AddItem)
                .WithName("AddItem")
                .WithSummary("Add item to basket");

            group.MapDelete("/remove", RemoveItem)
                .WithName("RemoveItem")
                .WithSummary("Remove item from basket");


            group.MapPost("/merge", MergeBasket)
              .WithName("MergeBasket")
              .WithSummary("Merge Basket");
        }

        private static async Task<IResult> GetBasket(HttpContext context, [FromBody] GetBasketRequest req, [FromServices] IBasketService basketService)
        {
            if (context.IsAuthenticated())
                req.BasketId = context.GetUserId();

            var basket = await basketService.GetBasketAsync(req.BasketId!,true);

            return Results.Ok(new
            {
                BasketId = context.IsAuthenticated() ? null : req.BasketId,
                Basket = basket
            });
        }

        private static async Task<IResult> AddItem(HttpContext context,[FromBody] RemoveBasketItemRequest req,[FromServices] IBasketService basketService, [FromServices] IValidator<RemoveBasketItemRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(req);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }

            if (context.IsAuthenticated())
                req.BasketId = context.GetUserId()!;

            var basket = await basketService.AddItemAsync(req);
            return Results.Ok(new
            {
                BasketId = context.IsAuthenticated() ? null : req.BasketId,
                Basket = basket
            });
        }

        private static async Task<IResult> RemoveItem(HttpContext context,[FromBody] RemoveBasketItemRequest req,[FromServices] IBasketService basketService, [FromServices] IValidator<RemoveBasketItemRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(req);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }

            if (context.IsAuthenticated())
                req.BasketId = context.GetUserId()!;

            var basket = await basketService.RemoveItemAsync(req);

            if (basket == null)
                return Results.NotFound();

            return Results.Ok(new
            {
                BasketId = context.IsAuthenticated() ? null : req.BasketId,
                Basket = basket
            });
        }

        private static async Task<IResult> MergeBasket(HttpContext context, [FromBody] GetBasketRequest req, [FromServices] IBasketService basketService)
        {
            var userId = context.GetUserId();
            var basket = await basketService.MergeBasketsAsync(req.BasketId!, userId!);

            if (basket == null)
                return Results.NotFound();

            return Results.Ok(new
            {
                BasketId = context.IsAuthenticated() ? null : req.BasketId,
                Basket = basket
            });
        }
    }
}
