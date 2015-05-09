namespace Paging.Buttons
{
    public class Previous : Button
    {
        public Previous(int pageNumber, int currentPageNumber, int totalNumberPages, string actionUrl, string parameters) :
            base(pageNumber, currentPageNumber, totalNumberPages, actionUrl, parameters)
        {
            IsSelected = (CurrentPageNumber == 1);
        }

        public override string Draw()
        {
            string navbar = IsSelected ? "<li class='disabled'><a href='#' aria-label='Previous'>" : string.Format("<li><a href='{0}={1}{2}' aria-label='Previous'>", ActionUrl, CurrentPageNumber - 1, Parameters);

            return string.Concat(navbar, "<span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span></a></li>");
        }
    }
}
