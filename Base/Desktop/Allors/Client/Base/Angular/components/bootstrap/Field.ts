namespace Allors.Bootstrap {
    export abstract class Field {
        label;
        placeholder;
        help;
        
        private f;
        private o;
        private r;
        private d;
        private l: (any) => void;

        constructor(public $log: angular.ILogService, public $translate: angular.translate.ITranslateService) {
        }

        get form(): Bootstrap.FormController {
            return this.f;
        }

        set form(value: Bootstrap.FormController) {
            this.f = value;
            this.onBind();
        }
        
        get object(): SessionObject {
            return this.o;
        }

        set object(value: SessionObject) {
            this.o = value;
            this.onBind();
        }

        get roleTypeName(): string {
            return this.r;
        }

        set roleTypeName(value: string) {
            this.r = value;
            this.onBind();
        }

        get displayName(): string {
            return this.d;
        }

        set displayName(value: string) {
            this.d = value;
            this.onBind();
        }

        get boundLookup(): (any) => void {
            return this.l;
        }

        set boundLookup(value: (any) => void) {
            this.l = value;
            this.onBind();
        }

        get objectType(): Meta.ObjectType {
            return this.object && this.object.objectType;
        }

        get roleType(): Meta.RoleType {
            return this.object && this.object.objectType.roleTypeByName[this.roleTypeName];
        }

        get canRead(): boolean {
            let canRead = false;
            if (this.object) {
                canRead = this.object.canRead(this.roleTypeName);
            }

            return canRead;
        }

        get canWrite(): boolean {
            let canWrite = false;
            if (this.object) {
                canWrite = this.object.canWrite(this.roleTypeName);
            }

            return canWrite;
        }
        
        get role(): any {
            return this.object && this.object[this.roleTypeName];
        }

        set role(value: any) {
            this.object[this.roleTypeName] = value;
        }

        get display(): any {
            return this.role && this.role[this.displayName];
        }

        onBind() {
            if (this.roleType) {
                if (this.label === undefined) {
                    this.label = null;

                    const key1 = `meta_${this.objectType.name}_${this.roleType.name}_Label`;
                    const key2 = `meta_${this.roleType.objectType}_${this.roleType.name}_Label`;
                    this.translate(key1, key2, (value) => this.label = value, () => this.label = this.roleTypeName);
                }

                if (this.placeholder === undefined) {
                    this.placeholder = null;

                    const key1 = `meta_${this.objectType.name}_${this.roleType.name}_Placeholder`;
                    const key2 = `meta_${this.roleType.objectType}_${this.roleType.name}_Placeholder`;
                    this.translate(key1, key2, (value) => this.placeholder = value);
                }

                if (this.help === undefined) {
                    this.help = null;

                    const key1 = `meta_${this.objectType.name}_${this.roleType.name}_Help`;
                    const key2 = `meta_${this.roleType.objectType}_${this.roleType.name}_Help`;
                    this.translate(key1, key2, (value) => this.help = value);
                }
            }
        };

        translate(key1: string, key2: string, set: (translation: string) => void, setDefault?: () => void) {
            this.$translate(key1)
                .then(translation => {
                    if (key1 !== translation) {
                        set(translation);
                    } else {
                        this.$translate(key2)
                            .then(translation => {
                                if (key2 !== translation) {
                                    set(translation);
                                } else {
                                    if (setDefault) {
                                        setDefault();
                                    }
                                }
                            });
                    }
                });

        }
    }
}
