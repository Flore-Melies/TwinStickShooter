# TwinStickShooter

## Consigne

Reproduire le jeu trouvable [ici](https://github.com/Flore-Melies/TwinStickShooter/releases).

Le pack d'asset est disponible [ici](https://www.kenney.nl/assets/topdown-shooter).
Le pixel per unit de reference du pack d'asset est de 64.

## Pré-requis

Il est impératif d'utiliser les paquets suivants dans votre projet :
- Universal RP
- Input System
- [2D tilemap extras](https://github.com/Unity-Technologies/2d-extras)
- Cinemachine

## Conseils

Pour récupérer un vecteur équivalent à la direction entre 2 points, la formule est : Point d'arrivée - point de départ.
À ce titre la direction d'un objet A à la position (5,5) vers un objet B à la position (7,9) sera la suivante :
(7-5,9-5) = (2,4)

Il faut noter aussi la possibilité de réduire la taille de ce vecteur à 1 en le [normalisant](https://docs.unity3d.com/ScriptReference/Vector2-normalized.html) à la suite des calculs.

Enfin pour faire tourner un objet dans une direction donnée, vous pouvez utiliser [ce code d'exemple](http://answers.unity.com/answers/779428/view.html).