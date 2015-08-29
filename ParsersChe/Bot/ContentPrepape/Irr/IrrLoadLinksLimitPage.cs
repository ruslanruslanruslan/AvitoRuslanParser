using ParsersChe.WebClientParser;

namespace ParsersChe.Bot.ContentPrepape.Irr
{
  public class IrrLoadLinksLimitPage : IrrLoadLinks
  {
    private int limitPage = 0;
    public IrrLoadLinksLimitPage(IHttpWeb webCl, int limitPage)
      : base(webCl)
    {

    }

    public override void LoadLinkWithAllPage()
    {
      if (NumberPage >= limitPage)
        IsNextPage = false;
      base.LoadLinkWithAllPage();
    }
    protected override void PrepareUrl()
    {
      if (NumberPage >= limitPage - 1)
        IsNextPage = false;
      base.PrepareUrl();
    }
  }
}
