using Microsoft.AspNetCore.Components;

namespace MeihuaWintry.Pages;

public partial class CaseEditPage
{
    private DisplayedCase? editingCase;

    [Parameter]
    public Guid CaseId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var c = this.CaseStore.GetCase(this.CaseId);
        if (c == null)
        {
            this.editingCase = null;
        }
        else
        {
            var z = await this.ZhouyiProvider.GetStoreAsync();
            this.editingCase = new(c, z);
            this.Save(true);
        }
    }

    private void Save(bool noPrompt = false)
    {
        if (this.editingCase is not null)
            this.CaseStore.UpdateCase(this.editingCase.InnerCase);
        if (!noPrompt)
            this.Snackbar.Add("保存成功");
    }
}
