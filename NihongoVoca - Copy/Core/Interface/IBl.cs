using System.Collections.Generic;

namespace Ivs.Core.Interface
{
    public interface IBl
    {
        int SearchData(IDto searchDto, out System.Data.DataTable dtResult);

        int UpdateData(IDto updateDto);

        int InsertData(IDto insertDto);

        int DeleteData(List<IDto> listDto);
    }
}