namespace Allors
{
	export interface ICommandNotifier 
    {
        invoke(invokeResponse: Data.InvokeResponse);

        save(saveResponse: Data.SaveResponse);
	}
}
