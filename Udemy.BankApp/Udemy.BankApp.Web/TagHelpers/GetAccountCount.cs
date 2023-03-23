using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;

namespace Udemy.BankApp.Web.TagHelpers
{
    [HtmlTargetElement("getAccountCount")]
    public class GetAccountCount : TagHelper
    {
        public int ApplicationUserId { get; set; }

        //dependecnt injection
        private readonly BankContext _context;
        public GetAccountCount(BankContext context)
        {
            _context = context;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var acoountCount = _context.Accounts.Count(x=> x.ApplicationUserId == ApplicationUserId);
            var html = $"<span class='badge bg-danger'>{acoountCount} </span>";
            output.Content.SetHtmlContent(html);
        }
    }
}
