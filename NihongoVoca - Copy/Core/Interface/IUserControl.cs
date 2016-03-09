namespace Ivs.Core.Interface
{
    public interface IUserControl
    {
        void InitControl();

        void LoadControlData();

        void SetControl();

        void SetLanguage();

        void Reset();

        void ClearError();
    }
}