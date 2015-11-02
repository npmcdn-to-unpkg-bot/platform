module App
{
    export class Profile {
        filterYear: number;
        filterMonth: number;

        tinymceOptions: any;
    }

    export class ProfileService {
        public static defaultFiscalYear = 2015; 

        private static key = "Profile";

        profile: Profile;

        static $inject = ["$window"];
        constructor(private $window: ng.IWindowService) {
            if (localStorage) {
                var json = localStorage.getItem(ProfileService.key);
                this.profile = json ? JSON.parse(json) : new Profile(); 
            } else {
                this.profile = new Profile();
            }

            this.ensureDefaults();

            window.onbeforeunload = () => {
               this.save();
            };
        }

        save(): void {
            if (localStorage) {
                var json = JSON.stringify(this.profile);
                localStorage.setItem(ProfileService.key, json);
            }
        }

        private ensureDefaults() {
            if (!this.profile.filterYear) {
                this.profile.filterYear = ProfileService.defaultFiscalYear;
            }

            if (!this.profile.tinymceOptions) {
                this.profile.tinymceOptions = {
                    plugins: "link",
                    toolbar: "styleselect | bullist bold italic | link",
                    skin: 'lightgray',
                    theme: 'modern',
                    menubar: false,
                };
            }
        }

    }

    angular
        .module("app")
        .service("profileService",
        App.ProfileService);
}