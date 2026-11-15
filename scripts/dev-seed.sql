-- Fictional development data. Fixed UUIDs make this safe to run repeatedly.

INSERT INTO addresses (id, line_1, line_2, town, county, eircode)
VALUES
    ('10000000-0000-4000-8000-000000000001', 'Apartment 18, Hazel Court', NULL, 'Clonee', 'Dublin', 'D15 A101'),
    ('10000000-0000-4000-8000-000000000002', '2 Cois Glaisín Close', 'Johnstown', 'Navan', 'Meath', 'C15 A102'),
    ('10000000-0000-4000-8000-000000000003', '8 Oak Avenue', NULL, 'Navan', 'Meath', 'C15 A103'),
    ('10000000-0000-4000-8000-000000000004', '12 Maple Drive', NULL, 'Clonee', 'Dublin', 'D15 A104'),
    ('10000000-0000-4000-8000-000000000005', '5 Willow Park', 'Johnstown', 'Navan', 'Meath', 'C15 A105'),
    ('10000000-0000-4000-8000-000000000006', '21 Riverbank Road', NULL, 'Trim', 'Meath', 'C15 A106')
ON CONFLICT (id) DO NOTHING;

INSERT INTO users (id, first_name, last_name, email, phone_number, address_id, is_verified)
VALUES
    ('20000000-0000-4000-8000-000000000001', 'Michael', 'Grinnell', 'michael.grinnell@example.com', '+353 1 555 0101', '10000000-0000-4000-8000-000000000003', true),
    ('20000000-0000-4000-8000-000000000002', 'Aoife', 'Byrne', 'aoife.byrne@example.com', '+353 1 555 0102', '10000000-0000-4000-8000-000000000004', false),
    ('20000000-0000-4000-8000-000000000003', 'Patrick', 'and Cara', 'patrick.cara@example.com', '+353 1 555 0103', '10000000-0000-4000-8000-000000000001', true),
    ('20000000-0000-4000-8000-000000000004', 'Ciarán', 'Murphy', 'ciaran.murphy@example.com', '+353 1 555 0104', '10000000-0000-4000-8000-000000000005', true),
    ('20000000-0000-4000-8000-000000000005', 'Niamh', 'Kelly', 'niamh.kelly@example.com', '+353 1 555 0105', '10000000-0000-4000-8000-000000000006', false)
ON CONFLICT (id) DO NOTHING;

INSERT INTO properties (
    id, address_id, property_type, bedrooms, bathrooms, year_built,
    ber_rating, floor_area_sqm, description, created_at, updated_at)
VALUES
    (
        '30000000-0000-4000-8000-000000000001', '10000000-0000-4000-8000-000000000001',
        'apartment', 2, 2, 2014, 'B3', 68.00,
        'A well-kept two-bedroom apartment in Clonee, Dublin 15. Bright open-plan living area, modern kitchen, main bedroom with ensuite, and good transport links to Blanchardstown and Dublin city.',
        CURRENT_TIMESTAMP - INTERVAL '35 days', CURRENT_TIMESTAMP - INTERVAL '3 days'
    ),
    (
        '30000000-0000-4000-8000-000000000002', '10000000-0000-4000-8000-000000000002',
        'semi_detached', 4, 3, 2018, 'A3', 125.00,
        'A bright and well-maintained semi-detached home in Johnstown, Navan. Modern kitchen, generous living space, four bedrooms, and a rear garden. Close to schools, shops, and local services.',
        CURRENT_TIMESTAMP - INTERVAL '26 days', CURRENT_TIMESTAMP - INTERVAL '2 days'
    )
ON CONFLICT (id) DO NOTHING;

INSERT INTO listings (id, property_id, seller_id, asking_price, status, created_at, updated_at)
VALUES
    (
        '40000000-0000-4000-8000-000000000001', '30000000-0000-4000-8000-000000000001',
        '20000000-0000-4000-8000-000000000003', 325000.00, 'listed',
        CURRENT_TIMESTAMP - INTERVAL '30 days', CURRENT_TIMESTAMP - INTERVAL '3 days'
    ),
    (
        '40000000-0000-4000-8000-000000000002', '30000000-0000-4000-8000-000000000002',
        '20000000-0000-4000-8000-000000000001', 425000.00, 'listed',
        CURRENT_TIMESTAMP - INTERVAL '21 days', CURRENT_TIMESTAMP - INTERVAL '2 days'
    )
ON CONFLICT (id) DO NOTHING;
