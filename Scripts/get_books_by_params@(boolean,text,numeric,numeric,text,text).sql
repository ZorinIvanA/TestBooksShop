create function get_books_by_params(_exists boolean, _types text, _mincost numeric, _maxcost numeric, _author text, _name text)
    returns TABLE(id integer, name character varying, author character varying, price numeric, book_type integer, storage integer)
    language plpgsql
as
$$
BEGIN
    RETURN QUERY SELECT b.id, b.name, b.author, b.price, b.type, b.present_on_storage
                 FROM common.books b
                 WHERE CASE
                           WHEN _exists IS NOT NULL THEN (b.present_on_storage > 0) = _exists
                           ELSE b.present_on_storage >= 0
                     END
                   AND CASE
                           WHEN _types IS NOT NULL THEN b.type = ANY
                                                        (ARRAY(SELECT j::int FROM jsonb_array_elements_text(to_jsonb(_types::json)) j))
                           ELSE b.type > 0
                     END
                   AND b.price >= _minCost
                   AND b.price <= _maxCost
                   AND CASE
                           WHEN _author IS NOT NULL THEN position(_author in b.author) > 0
                           ELSE b.author LIKE '%'
                     END
                   AND CASE
                           WHEN _name IS NOT NULL THEN position(_name in b.name) > 0
                           ELSE b.name LIKE '%'
                     END;
END;
$$;

alter function get_books_by_params(boolean, text, numeric, numeric, text, text) owner to postgres;

