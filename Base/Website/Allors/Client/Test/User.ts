module Allors.Domain.Test
{
	export class User implements Allors.Domain.User
    {
        UserEmail: string;

        get Mail(): string {
            return this.UserEmail;
        }
	}
}
