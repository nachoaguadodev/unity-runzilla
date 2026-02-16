# Runzilla Endless Runner

Desarrollado en Unity con C#. 
Un *Endless Runner* donde la precisión y los reflejos son clave.

![RunzillaDemoGIF](https://github.com/user-attachments/assets/591c2869-a335-41ad-9f01-2f7812faf003)

## Características Principales

¡Compite por la mejor puntuación en un mundo infinito generado proceduralmente!

* **Mecánicas Ágiles:** Salto variable sensible a la presión y caídas rápidas con *snappy physics*.
* **Doble Desafío:** Enfréntate a enemigos terrestres y aéreos o esquiva obstáculos estáticos.
* **Progresión:** Sistema de *High Score* guardado y dificultad progresiva por aumento de velocidad.
* **Estética Retro:** Pixel art con paleta de 4 colores y efectos de *Parallax*.

---

## Ficha Técnica (Under the Hood)

Detalles técnicos sobre la implementación del código y la arquitectura del proyecto:

### Core & Mecánicas
* **Godzilla Controller:** Física de salto ajustada con gravedad personalizada. Detección de suelo precisa mediante **Doble Raycast** para evitar errores en bordes.
* **Ragdoll Death:** Secuencia de muerte con físicas activadas, rotación e impulso al colisionar.
* **Inputs:** Sistema de control reactivo y adaptado para teclado/ratón (preparado para móvil).

### Generación
* **Smart Spawning:** Algoritmo que alterna entre carriles para variar el gameplay.
* **Sistema Anti-Frustración:** Uso de `Physics2D.OverlapCircle` antes de instanciar enemigos para evitar solapamientos injustos con obstáculos.
* **Optimización:** Scripts de auto-destrucción para objetos fuera de cámara (*Culling* manual).

### Datos & Sistemas
* **Patrón Singleton:** Implementado en `GameManager`, `BGMOVEMENT` y `Puntuacion` para una gestión centralizada y eficiente.
* **Persistencia:** Guardado de Récords mediante `PlayerPrefs`.
* **UI Reactiva:** Uso de `Time.unscaledTime` para animaciones de interfaz Scale tweening incluso cuando el juego está en pausa.

### Audio & Visuales
* **Audio Manager:** Gestión dinámica de pistas (Gameplay vs Game Over) y SFX.
* **Parallax Scrolling:** Fondo multicapa con movimiento independiente.
* **UI Adaptativa:** Canvas con *Scale With Screen Size* para soporte de múltiples resoluciones.

---

## Controles
* **ESPACIO:** Saltar (Mantener para mayor altura).
