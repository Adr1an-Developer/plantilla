<h1 style="color:blue;"> Proyecto Panel Administrativo para sistema de loterias.</h1>

## Generación de Proyecto

> - ng new softgeek-lottery-panel --directory=. --style=scss
> - ng add @angular/material
> - npm i jwt-decode

## Para Generación de carpetas internas modelo DDD

> Para la generación de las carpetas usares el script en powerShell `CreateDirectories.ps1` ubicado en la carpeta raiz de este proyecto, que creará todos los directorios requeridos para el modelo DDD.

## Componentes fuera del Layout

> ### Componente Auth
>
> - ng g c auth/ui/login --skip-tests
> - ng g c auth/ui/register --skip-tests

## Componentes Modulos

> ### Componente Dashboard
>
> - ng g c modules/dashboards/ui/dashboard --skip-tests

> ### Componente Configuración
>
> - ng g c modules/config/ui/menu --skip-tests
> - ng g c modules/config/ui/menu/menu-detail --skip-tests
> - ng g c modules/config/ui/access --skip-tests
> - ng g c modules/config/ui/access/access-detail --skip-tests
> - ng g c modules/config/ui/users --skip-tests
> - ng g c modules/config/ui/users/user-detail --skip-tests
> - ng g c modules/config/ui/profiles --skip-tests
> - ng g c modules/config/ui/profiles/profiles-detail --skip-tests

## Componentes Shared

> ### Componente Layout
>
> - ng g c shared/components/layout --skip-tests
> - ng g c shared/components/layout/header --skip-tests
> - ng g c shared/components/layout/footer --skip-tests
> - ng g c shared/components/layout/sidenav --skip-tests

> ### Componente Notificaciones
>
> - ng generate component shared/components/notification --skip-tests

> ### Componente Breadcrumbs
>
> - ng generate component shared/components/breadcrumbs --skip-tests

> ### Componente Filtros
>
> - ng generate component shared/components/filter --skip-tests

> ### Componente Dialogos
>
> - ng generate component shared/components/confirm-dialog --skip-tests
