// Step-Function for Cartoon Shading
// Verwendet die Properties _Step_?_*
// Diese müssen vor dem include-Statement vereinbart sein!
//
// Autor: Manfred Brill

float step(float value)
{
    float factor = float(0.0);
    if (value > _Step_x_1) factor = _Step_y_1;
    if (value > _Step_x_2) factor = _Step_y_2;
    if (value > _Step_x_3) factor = _Step_y_3;
    return factor;
}
