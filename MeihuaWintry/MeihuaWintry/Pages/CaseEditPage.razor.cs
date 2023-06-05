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
            this.editingCase = null;
        else
        {
            var z = await this.ZhouyiProvider.GetStoreAsync();
            this.editingCase = c is null ? null : new(c, z);
            Save();
        }
    }

    DateTime lastSaveTime;
    private void Save()
    {
        if (editingCase is not null)
            CaseStore.UpdateCase(editingCase.InnerCase);
        lastSaveTime = DateTime.Now;
    }
}
