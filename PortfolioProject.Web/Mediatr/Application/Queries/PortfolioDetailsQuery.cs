﻿using MediatR;
using PortfolioProject.Web.Mediatr.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioProject.Web.Mediatr.Application.Queries
{
    public class PortfolioDetailsQuery : IRequest<PortfolioResponse>
    {
    }
}
