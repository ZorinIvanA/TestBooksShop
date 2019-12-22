create function get_sales_by_types(_startdate timestamp without time zone, _finishdate timestamp without time zone)
    returns TABLE(book_type integer, sold_count bigint)
    language plpgsql
as
$$
BEGIN
    RETURN QUERY SELECT b.type, count(s.book_id) as sold_count
                 FROM common.sales s INNER JOIN common.books b on s.book_id = b.id
                      --WHERE s.sale_date BETWEEN _startDate AND _finishDate
                 GROUP BY b.type;
END
$$;

alter function get_sales_by_types(timestamp, timestamp) owner to postgres;

