﻿using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries
{
    //Geriye ProductListDto dönecek liste olarak değil dikkatinizi çekerim.
    public class GetCategoryQueryByIdRequest : IRequest<CategoryListDto>
    {
        //bu classtan bir ıd parametresi bekliyoruz
        public int Id { get; set; }
        //bana bir id değeri gelsin ve ben bu gelen id değerini bu classın içindeki id propuna atayım.
        public GetCategoryQueryByIdRequest(int id)
        {
            this.Id = id;  
        }
    }
}